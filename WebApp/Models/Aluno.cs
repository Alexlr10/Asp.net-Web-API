using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebApp.Models {
    public class Aluno {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
        public int ra { get; set; }
        public List<Aluno> listarAlunos() {
            //Pega o arquivo Json no diretorio do projeto
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            //Pega o texto e insere na  variavel json
            var json = File.ReadAllText(caminhoArquivo);
            // Pega a lista json e desirializa para uma lista de alunos
            var listaAlunos = JsonConvert.DeserializeObject<List<Aluno>>(json);

            return listaAlunos;
        }
        public bool RescreverArquivo(List<Aluno> listaAlunos) {
            //Pega o arquivo Json no diretorio do projeto
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            // Pega a lista json e serializa em texto
            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            // Escreve o texto da variavel json no caminho do arquivo e retorna true
            File.WriteAllText(caminhoArquivo, json);

            return true;
        }

        public Aluno Inserir(Aluno Aluno) {
            //Pega todos os alunos e joga na variavel listaAlunos
            var listaAlunos = this.listarAlunos();
            //Recupera o id de valor maior na lista de alunos
            var maxId = listaAlunos.Max(aluno => aluno.id);
            //Vai incrementar o id em +1
            Aluno.id = maxId + 1;
            //vai salvar o aluno na lista
            listaAlunos.Add(Aluno);
            //O metodo ira adicionar o aluno a lista 
            RescreverArquivo(listaAlunos);
            return Aluno;
        }

        public Aluno Atualizar(int id, Aluno Aluno) {
            //Pega todos os alunos e joga na variavel listaAlunos
            var listaAlunos = this.listarAlunos();
            // FinxIndex pega o indice do objeto que passo no parametro
            var itemIndex = listaAlunos.FindIndex(aluno => aluno.id == Aluno.id);
            //Se existir o indice, atualiza o objeto e salva na mesma pocisao do array          
            if (itemIndex >= 0) {
                Aluno.id = id;
                listaAlunos[itemIndex] = Aluno;
            } else { // Senao existir retorna null
                return null;
            }
            //Rescrever a listagem apos atualizar
            RescreverArquivo(listaAlunos);
            return Aluno;
        }

        public bool Deletar(int id) {
            //Pega todos os alunos e joga na variavel listaAlunos
            var listaAlunos = this.listarAlunos();
            // FinxIndex pega o indice do objeto que passo no parametro
            var itemIndex = listaAlunos.FindIndex(aluno => aluno.id == id);
            //Se existir o indice, deleta o objeto do array          
            if (itemIndex >= 0) {
                listaAlunos.RemoveAt(itemIndex);
            } else { // Senao existir retorna null
                return false;
            }
            //Rescrever a listagem apos deletar
            RescreverArquivo(listaAlunos);
            return true;
        }
    }
}