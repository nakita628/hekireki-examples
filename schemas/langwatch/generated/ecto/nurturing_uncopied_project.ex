defmodule LangWatch.NurturingUncopiedProject do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          project_id: String.t(),
          first_seen_at: DateTime.t()
        }

  schema "NurturingUncopiedProject" do
    field(:project_id, :string, primary_key: true, source: :projectId)
    field(:first_seen_at, LangWatch.PrismaDateTime, autogenerate: true, source: :firstSeenAt)
  end
end