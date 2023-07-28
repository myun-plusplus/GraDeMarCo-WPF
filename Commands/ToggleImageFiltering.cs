﻿using GraDeMarCoWPF.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GraDeMarCoWPF.Commands
{
    public class ToggleImageFiltering : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AppData appData;
        private IImageFiltering imageFiltering;
        private IImageBinarizing imageBinarizing;
        private IGrainDetecting grainDetecting;

        public ToggleImageFiltering(
            AppData appData,
            IImageFiltering imageFiltering,
            IImageBinarizing imageBinarizing,
            IGrainDetecting grainDetecting)
        {
            this.appData = appData;
            this.imageFiltering = imageFiltering;
            this.imageBinarizing = imageBinarizing;
            this.grainDetecting = grainDetecting;

            appData.PropertyChanged += this.appData_PropertyChanged;
        }

        public bool CanExecute(object parameter)
        {
            return appData.CurrentState == AppState.ImageOpened ||
                appData.CurrentState == AppState.ImageFiltering;
        }

        public void Execute(object parameter)
        {
            if ((parameter as bool?) ?? false)
            {
                appData.CurrentState = AppState.ImageFiltering;
                imageFiltering.StartFunction();
                imageFiltering.FilterOriginalImage();
                imageBinarizing.BinarizeFilteredImage();
                grainDetecting.DetectGrains();
            }
            else
            {
                appData.CurrentState = AppState.ImageOpened;
                imageFiltering.StopFunction();
                imageFiltering.FilterOriginalImage();
                imageBinarizing.BinarizeFilteredImage();
                grainDetecting.DetectGrains();
            }
        }

        private void appData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }
    }
}
