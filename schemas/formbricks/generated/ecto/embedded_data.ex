defmodule Formbricks.EmbeddedData do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          key: String.t() | nil,
          name: String.t(),
          description: String.t() | nil,
          source: atom(),
          data_type: atom(),
          default_value: map() | nil,
          locked: boolean(),
          survey_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Formbricks.Workspace.t() | nil
        }

  schema "EmbeddedData" do
    field(:id, :string, primary_key: true)
    field(:key, :string)
    field(:name, :string)
    field(:description, :string)
    field(:source, Ecto.Enum, values: [:computed, :ingested, :reserved])
    field(:data_type, Ecto.Enum, values: [:string, :number, :boolean, :date], default: :string, source: :dataType)
    field(:default_value, :map, source: :defaultValue)
    field(:locked, :boolean, default: false)
    field(:survey_id, :string, source: :surveyId)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end