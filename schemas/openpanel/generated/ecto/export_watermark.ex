defmodule OpenPanel.ExportWatermark do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          last_inserted_at: DateTime.t(),
          last_event_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: OpenPanel.Project.t() | nil,
          integration: OpenPanel.Integration.t() | nil
        }

  schema "export_watermarks" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:last_inserted_at, OpenPanel.PrismaDateTime, source: :lastInsertedAt)
    field(:last_event_id, :string, default: "", source: :lastEventId)
    field(:project_id, :string, source: :projectId)
    field(:integration_id, :string, source: :integrationId)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:integration, OpenPanel.Integration, foreign_key: :integration_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end