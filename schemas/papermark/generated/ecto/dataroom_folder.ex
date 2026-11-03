defmodule Papermark.DataroomFolder do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          path: String.t(),
          icon: String.t() | nil,
          color: String.t() | nil,
          order_index: integer() | nil,
          hierarchical_index: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          parent_folder: Papermark.DataroomFolder.t() | nil,
          dataroom: Papermark.Dataroom.t() | nil,
          documents: [Papermark.DataroomDocument.t()],
          child_folders: [Papermark.DataroomFolder.t()],
          upload_tasks: [Papermark.Task.t()]
        }

  schema "DataroomFolder" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:path, :string)
    field(:icon, :string)
    field(:color, :string)
    field(:order_index, :integer, source: :orderIndex)
    field(:hierarchical_index, :string, source: :hierarchicalIndex)
    field(:parent_id, :string, source: :parentId)
    field(:dataroom_id, :string, source: :dataroomId)
    belongs_to(:parent_folder, Papermark.DataroomFolder, foreign_key: :parent_id, define_field: false, type: :string)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    has_many(:documents, Papermark.DataroomDocument, foreign_key: :folder_id)
    has_many(:child_folders, Papermark.DataroomFolder, foreign_key: :parent_id)
    has_many(:upload_tasks, Papermark.Task, foreign_key: :upload_folder_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end