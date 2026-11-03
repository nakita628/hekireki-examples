defmodule Typebot.RuntimeMediaIdCache do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          public_typebot_id: String.t(),
          provider: atom(),
          url: String.t(),
          media_id: String.t(),
          expires_at: DateTime.t() | nil,
          public_typebot: Typebot.PublicTypebot.t() | nil
        }

  schema "RuntimeMediaIdCache" do
    field(:provider, Ecto.Enum, values: [:WHATSAPP, :DIALOG360])
    field(:url, :string, primary_key: true)
    field(:media_id, :string, source: :mediaId)
    field(:expires_at, Typebot.PrismaDateTime, source: :expiresAt)
    field(:public_typebot_id, :string, primary_key: true, source: :publicTypebotId)
    belongs_to(:public_typebot, Typebot.PublicTypebot, foreign_key: :public_typebot_id, define_field: false, type: :string)
  end
end