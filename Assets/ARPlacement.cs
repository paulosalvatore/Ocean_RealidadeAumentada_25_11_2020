using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    /*
     * Receber os objetos que estão na Unity (aranha, target marker e RaycastManager)
     * Aranha: iremos criar uma nova toda vez que o usuário clicar na tela
     * TargetMarker: iremos posicioná-lo exatamente no ponto em que a aranha será criada
     *              Ele será posicionado sempre que o usuário estiver apontando o celular
     *              para um ponto válido do "plane" (detectado pelo Plane Manager)
     * RaycastManager: É utilizado para detectar quando o usuário está apontando o celular
     *                 para o plane.
    */

    public GameObject spider;

    public GameObject targetMarker;

    public ARRaycastManager rayManager;

    void Update()
    {
        // Pega o valor de X e Y baseado na largura/altura da tela divididas por 2, pegando o ponto central
        var x = Screen.width / 2;
        var y = Screen.height / 2;

        // Constrói um Vector2 a partir de X e Y
        var screenCenter = new Vector2(x, y);

        // Prepara uma lista vazia para colocar resultados dos toques no plano que foi gerado em tempo real
        var hitResults = new List<ARRaycastHit>();

        // Emite um raio a partir do centro da tela do celular em direção ao plano.
        rayManager.Raycast(screenCenter, hitResults, TrackableType.PlaneWithinBounds);

        // Caso tenha algum toque no plano (a partir do raio que foi gerado), entra no if
        if (hitResults.Count > 0)
        {
            // Obtém o primeiro toque
            var hitResult = hitResults[0];

            // Atualiza a posição e a rotação do TargetMarker
            transform.position = hitResult.pose.position;
            transform.rotation = hitResult.pose.rotation;

            // Também é possível mudar posição e rotação ao mesmo tempo, caso queira ter mais performance
            // transform.SetPositionAndRotation(hitResult.pose.position, hitResult.pose.rotation);

            // Caso o TargetMarker não esteja ativo, ativamos ele
            if (!targetMarker.activeSelf) {
                targetMarker.SetActive(true);
            }
        }
    }
}
