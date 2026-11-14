defmodule LangWatch.ScimSsoConnectionView do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          folded: map(),
          applied_event_ids: [String.t()],
          projection_version: String.t(),
          created_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "ScimSsoConnectionView" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:folded, :map)
    field(:applied_event_ids, {:array, :string}, source: :appliedEventIds)
    field(:projection_version, :string, source: :projectionVersion)
    field(:created_at, LangWatch.PrismaDateTime, source: :createdAt)
    field(:updated_at, LangWatch.PrismaDateTime, source: :updatedAt)
  end
end