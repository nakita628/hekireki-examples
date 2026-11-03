defmodule Dub.FolderWebhook do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          inserted_at: DateTime.t(),
          folder: Dub.Folder.t() | nil,
          webhook: Dub.Webhook.t() | nil
        }

  schema "FolderWebhook" do
    field(:id, :string, primary_key: true)
    field(:folder_id, :string, source: :folderId)
    field(:webhook_id, :string, source: :webhookId)
    belongs_to(:folder, Dub.Folder, foreign_key: :folder_id, define_field: false, type: :string)
    belongs_to(:webhook, Dub.Webhook, foreign_key: :webhook_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end