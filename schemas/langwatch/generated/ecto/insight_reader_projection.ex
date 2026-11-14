defmodule LangWatch.InsightReaderProjection do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          insight_id: String.t(),
          user_id: String.t(),
          seen_at: float() | nil,
          archived_at: float() | nil,
          kept_at: float() | nil,
          created_at: float(),
          updated_at: float(),
          occurred_at: float(),
          accepted_at: float(),
          last_event_id: String.t(),
          projection_version: String.t()
        }

  schema "InsightReaderProjection" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:insight_id, :string, source: :insightId)
    field(:user_id, :string, source: :userId)
    field(:seen_at, :float, source: :seenAt)
    field(:archived_at, :float, source: :archivedAt)
    field(:kept_at, :float, source: :keptAt)
    field(:created_at, :float, source: :createdAt)
    field(:updated_at, :float, source: :updatedAt)
    field(:occurred_at, :float, source: :occurredAt)
    field(:accepted_at, :float, source: :acceptedAt)
    field(:last_event_id, :string, source: :lastEventId)
    field(:projection_version, :string, source: :projectionVersion)
  end
end