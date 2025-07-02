using UnityEngine;

public class ArrowChange : MonoBehaviour
{
    [SerializeField] GameObject arrowUp;
    [SerializeField] GameObject arrowDown;
    [SerializeField] GameObject arrowNoChange;

    public void ChangeArrow(Company.PriceChangeDirection priceChangeDirection)
    {
        arrowDown.SetActive(false);
        arrowUp.SetActive(false);
        arrowNoChange.SetActive(false);

        switch (priceChangeDirection)
        {
            case Company.PriceChangeDirection.Up:
                arrowUp.SetActive(true);
                break;

            case Company.PriceChangeDirection.Down:
                arrowDown.SetActive(true);
                break;

            case Company.PriceChangeDirection.NoChange:
                arrowNoChange.SetActive(true);
                break;
        }
    }
}
