defmodule Ghostfolio.Platform do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t() | nil,
          url: String.t(),
          accounts: [Ghostfolio.Account.t()]
        }

  schema "Platform" do
    field(:name, :string)
    field(:url, :string)
    has_many(:accounts, Ghostfolio.Account, foreign_key: :platform_id)
  end
end