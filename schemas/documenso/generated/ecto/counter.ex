defmodule Documenso.Counter do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          value: integer()
        }

  schema "Counter" do
    field(:id, :string, primary_key: true)
    field(:value, :integer)
  end
end