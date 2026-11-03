defmodule Dub.LinkWebhook do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          inserted_at: DateTime.t(),
          link: Dub.Link.t() | nil,
          webhook: Dub.Webhook.t() | nil
        }

  schema "LinkWebhook" do
    field(:id, :string, primary_key: true)
    field(:link_id, :string, source: :linkId)
    field(:webhook_id, :string, source: :webhookId)
    belongs_to(:link, Dub.Link, foreign_key: :link_id, define_field: false, type: :string)
    belongs_to(:webhook, Dub.Webhook, foreign_key: :webhook_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end