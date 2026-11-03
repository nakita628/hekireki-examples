defmodule Dub.Dashboard do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          do_index: boolean(),
          password: String.t() | nil,
          show_conversions: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          link: Dub.Link.t() | nil,
          folder: Dub.Folder.t() | nil,
          project: Dub.Project.t() | nil,
          user: Dub.User.t() | nil
        }

  schema "Dashboard" do
    field(:id, :string, primary_key: true)
    field(:do_index, :boolean, default: false, source: :doIndex)
    field(:password, :string)
    field(:show_conversions, :boolean, default: false, source: :showConversions)
    field(:link_id, :string, source: :linkId)
    field(:folder_id, :string, source: :folderId)
    field(:project_id, :string, source: :projectId)
    field(:user_id, :string, source: :userId)
    belongs_to(:link, Dub.Link, foreign_key: :link_id, define_field: false, type: :string)
    belongs_to(:folder, Dub.Folder, foreign_key: :folder_id, define_field: false, type: :string)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:user, Dub.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end