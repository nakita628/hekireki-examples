defmodule Dub.ProjectUsers do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          role: atom(),
          workspace_preferences: map() | nil,
          default_folder_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Dub.User.t() | nil,
          project: Dub.Project.t() | nil,
          notification_preference: Dub.NotificationPreference.t() | nil
        }

  schema "ProjectUsers" do
    field(:id, :string, primary_key: true)
    field(:role, Ecto.Enum, values: [:owner, :member, :viewer, :billing], default: :member)
    field(:workspace_preferences, :map, source: :workspacePreferences)
    field(:default_folder_id, :string, source: :defaultFolderId)
    field(:user_id, :string, source: :userId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:user, Dub.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_one(:notification_preference, Dub.NotificationPreference, foreign_key: :project_user_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end