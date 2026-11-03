defmodule Papermark.Folder do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          path: String.t(),
          hidden_in_all_documents: boolean(),
          icon: String.t() | nil,
          color: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          parent_folder: Papermark.Folder.t() | nil,
          team: Papermark.Team.t() | nil,
          documents: [Papermark.Document.t()],
          child_folders: [Papermark.Folder.t()]
        }

  schema "Folder" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:path, :string)
    field(:hidden_in_all_documents, :boolean, default: false, source: :hiddenInAllDocuments)
    field(:icon, :string)
    field(:color, :string)
    field(:parent_id, :string, source: :parentId)
    field(:team_id, :string, source: :teamId)
    belongs_to(:parent_folder, Papermark.Folder, foreign_key: :parent_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    has_many(:documents, Papermark.Document, foreign_key: :folder_id)
    has_many(:child_folders, Papermark.Folder, foreign_key: :parent_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end