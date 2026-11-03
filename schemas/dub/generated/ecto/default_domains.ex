defmodule Dub.DefaultDomains do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          dubsh: boolean(),
          chatgpt: boolean(),
          sptifi: boolean(),
          gitnew: boolean(),
          callink: boolean(),
          amznid: boolean(),
          ggllink: boolean(),
          figpage: boolean(),
          loooooooong: boolean(),
          project: Dub.Project.t() | nil
        }

  schema "DefaultDomains" do
    field(:id, :string, primary_key: true)
    field(:dubsh, :boolean, default: true)
    field(:chatgpt, :boolean, default: true)
    field(:sptifi, :boolean, default: true)
    field(:gitnew, :boolean, default: true)
    field(:callink, :boolean, default: true)
    field(:amznid, :boolean, default: true)
    field(:ggllink, :boolean, default: true)
    field(:figpage, :boolean, default: true)
    field(:loooooooong, :boolean, default: false)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
  end
end