using UnityEngine;

public class BaseRules : MonoBehaviour
{
    /* Enable or disable base rule for player death (death when walk on red tiles) 
     * If false a rule must be added to the scene 
     */
    [SerializeField]
    public bool deathRule = true;

    /* Enable or disable base rule for winning (Player have walked on all tile 1 time) 
     * If false a rule must be added to the scene 
     */
    [SerializeField]
    public bool winningRule = true;

    /* Enable or disable base rule for tile check color
     * If false a rule must be added to the scene 
     */
    [SerializeField]
    public bool colorRule = true;
}
