defmodule OpenPanel.InsightEvent do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          event_kind: String.t(),
          change_from: map() | nil,
          change_to: map() | nil,
          inserted_at: DateTime.t(),
          insight: OpenPanel.ProjectInsight.t() | nil
        }

  schema "insight_events" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:event_kind, :string, source: :eventKind)
    field(:change_from, :map, source: :changeFrom)
    field(:change_to, :map, source: :changeTo)
    field(:insight_id, :string, source: :insightId)
    belongs_to(:insight, OpenPanel.ProjectInsight, foreign_key: :insight_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end