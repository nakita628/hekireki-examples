defmodule OpenPanel.EventMeta do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          conversion: boolean() | nil,
          color: String.t() | nil,
          icon: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: OpenPanel.Project.t() | nil
        }

  schema "event_meta" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:name, :string)
    field(:conversion, :boolean)
    field(:color, :string)
    field(:icon, :string)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end