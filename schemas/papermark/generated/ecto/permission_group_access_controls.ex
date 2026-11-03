defmodule Papermark.PermissionGroupAccessControls do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          item_id: String.t(),
          item_type: atom(),
          can_view: boolean(),
          can_download: boolean(),
          can_download_original: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          group: Papermark.PermissionGroup.t() | nil
        }

  schema "PermissionGroupAccessControls" do
    field(:id, :string, primary_key: true)
    field(:item_id, :string, source: :itemId)
    field(:item_type, Ecto.Enum, values: [:DATAROOM_DOCUMENT, :DATAROOM_FOLDER], source: :itemType)
    field(:can_view, :boolean, default: true, source: :canView)
    field(:can_download, :boolean, default: false, source: :canDownload)
    field(:can_download_original, :boolean, default: false, source: :canDownloadOriginal)
    field(:group_id, :string, source: :groupId)
    belongs_to(:group, Papermark.PermissionGroup, foreign_key: :group_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end