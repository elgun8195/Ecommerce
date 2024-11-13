using AzureProject.Business.Abstract;
using AzureProject.DataAccess.Abstract;
using AzureProject.DataAccess.UnitOfWork.Interface;
using AzureProject.Entity.Concrete;
using Newtonsoft.Json;

namespace AzureProject.Business.Concrete
{
    public class SliderManager : ISliderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISliderDal _sliderDal;

        public SliderManager(IUnitOfWork unitOfWork, ISliderDal sliderDal)
        {
            _unitOfWork = unitOfWork;
            _sliderDal = sliderDal;
        }

        public void SliderAdd(Slider slider)
        {
            if (slider == null)
            {
                throw new ArgumentNullException(nameof(slider), "Slider cannot be null.");
            }
            slider.CreatedTime = DateTime.Now;
            // Slider-i əlavə et
            _sliderDal.Insert(slider);
            // Dəyişiklikləri saxla
            _unitOfWork.SaveAsync();

        }



        public void SliderDelete(Slider slider)
        {
            if (slider == null)
            {
                throw new ArgumentNullException(nameof(slider), "Slider cannot be null.");
            }
            slider.DeletedTime = DateTime.Now;
            slider.IsDeleted = true;
            _sliderDal.Update(slider);
            _unitOfWork.SaveAsync();
        }

        public void SliderUpdate(Slider slider)
        {
            if (slider == null)
            {
                throw new ArgumentNullException(nameof(slider), "Slider cannot be null.");
            }
            slider.UpdatedTime = DateTime.Now;
            _sliderDal.Update(slider);
            _unitOfWork.SaveAsync(); // Dəyişiklikləri yaddaşa yazır
        }

        public Slider GetByIdSlider(int id)
        {
            return _sliderDal.Get(x => x.Id == id);
        }

        public List<Slider> GetList()
        {
            return _sliderDal.List(c => c.IsDeleted == false);
        }
    }
}
