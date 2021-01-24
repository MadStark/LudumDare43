public interface ISelectible
{
    bool IsSelectible { get; }

    void OnPointerEnter();

    void OnPointerExit();
}
