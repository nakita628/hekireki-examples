defmodule Dub.FolderUser do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          role: atom() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          folder: Dub.Folder.t() | nil,
          user: Dub.User.t() | nil
        }

  schema "FolderUser" do
    field(:id, :string, primary_key: true)
    field(:role, Ecto.Enum, values: [:owner, :editor, :viewer])
    field(:folder_id, :string, source: :folderId)
    field(:user_id, :string, source: :userId)
    belongs_to(:folder, Dub.Folder, foreign_key: :folder_id, define_field: false, type: :string)
    belongs_to(:user, Dub.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end