defmodule Formbricks.Chart do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          type: atom(),
          query: map(),
          config: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Formbricks.Workspace.t() | nil,
          creator: Formbricks.User.t() | nil,
          feedback_directory: Formbricks.FeedbackDirectory.t() | nil,
          widgets: [Formbricks.DashboardWidget.t()]
        }

  schema "Chart" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:type, Ecto.Enum, values: [:area, :bar, :pie, :big_number, :matrix])
    field(:query, :map, default: %{})
    field(:config, :map, default: %{})
    field(:workspace_id, :string, source: :workspaceId)
    field(:created_by, :string, source: :createdBy)
    field(:feedback_directory_id, :string, source: :feedbackDirectoryId)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    belongs_to(:creator, Formbricks.User, foreign_key: :created_by, define_field: false, type: :string)
    belongs_to(:feedback_directory, Formbricks.FeedbackDirectory, foreign_key: :feedback_directory_id, define_field: false, type: :string)
    has_many(:widgets, Formbricks.DashboardWidget, foreign_key: :chart_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end