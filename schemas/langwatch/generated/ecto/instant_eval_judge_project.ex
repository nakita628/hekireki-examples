defmodule LangWatch.InstantEvalJudgeProject do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          project_id: String.t(),
          organization_id: String.t(),
          created_at: DateTime.t()
        }

  schema "InstantEvalJudgeProject" do
    field(:project_id, :string, primary_key: true, source: :projectId)
    field(:organization_id, :string, source: :organizationId)
    field(:created_at, LangWatch.PrismaDateTime, source: :createdAt)
  end
end