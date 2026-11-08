defmodule Formbricks.Dashboard do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          filters: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Formbricks.Workspace.t() | nil,
          creator: Formbricks.User.t() | nil,
          widgets: [Formbricks.DashboardWidget.t()]
        }

  schema "Dashboard" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:filters, :map)
    field(:workspace_id, :string, source: :workspaceId)
    field(:created_by, :string, source: :createdBy)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    belongs_to(:creator, Formbricks.User, foreign_key: :created_by, define_field: false, type: :string)
    has_many(:widgets, Formbricks.DashboardWidget, foreign_key: :dashboard_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end