defmodule LangWatch.NurturingProjectUsageView do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          project_id: String.t(),
          owner: String.t(),
          folded: map(),
          applied_event_ids: [String.t()],
          projection_version: String.t(),
          updated_at: DateTime.t()
        }

  schema "NurturingProjectUsageView" do
    field(:project_id, :string, primary_key: true, source: :projectId)
    field(:owner, :string, primary_key: true)
    field(:folded, :map)
    field(:applied_event_ids, {:array, :string}, source: :appliedEventIds)
    field(:projection_version, :string, source: :projectionVersion)
    field(:updated_at, LangWatch.PrismaDateTime, source: :updatedAt)
  end
end