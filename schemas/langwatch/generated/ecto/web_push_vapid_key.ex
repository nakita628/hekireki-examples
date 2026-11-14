defmodule LangWatch.WebPushVapidKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          public_key: String.t(),
          private_key_encrypted: String.t(),
          inserted_at: DateTime.t()
        }

  schema "WebPushVapidKey" do
    field(:id, :string, primary_key: true, default: "self")
    field(:public_key, :string, source: :publicKey)
    field(:private_key_encrypted, :string, source: :privateKeyEncrypted)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end