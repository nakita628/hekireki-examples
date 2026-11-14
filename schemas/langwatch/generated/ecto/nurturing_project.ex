defmodule LangWatch.NurturingProject do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          project_id: String.t(),
          organization_id: String.t()
        }

  schema "NurturingProject" do
    field(:project_id, :string, primary_key: true, source: :projectId)
    field(:organization_id, :string, source: :organizationId)
  end
end