defmodule Typebot.SuppressedEmail do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          transient_general_bounce_count: integer(),
          suppressed_at: DateTime.t() | nil,
          last_webhook_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "SuppressedEmail" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:transient_general_bounce_count, :integer, default: 0, source: :transientGeneralBounceCount)
    field(:suppressed_at, Typebot.PrismaDateTime, source: :suppressedAt)
    field(:last_webhook_id, :string, source: :lastWebhookId)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end