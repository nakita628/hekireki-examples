defmodule Dub.JacksonIndex do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          key: String.t(),
          store_key: String.t()
        }

  schema "jackson_index" do
    field(:key, :string)
    field(:store_key, :string, source: :storeKey)
  end
end