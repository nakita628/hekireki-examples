defmodule Jitsu.KvStoreEntry do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          key: String.t(),
          value: map(),
          expires_at: DateTime.t() | nil
        }

  schema "kv" do
    field(:key, :string, primary_key: true)
    field(:value, :map)
    field(:expires_at, Jitsu.PrismaDateTime)
  end
end