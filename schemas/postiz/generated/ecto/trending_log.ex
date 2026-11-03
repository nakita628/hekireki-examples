defmodule Postiz.TrendingLog do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          language: String.t() | nil,
          date: DateTime.t()
        }

  schema "TrendingLog" do
    field(:language, :string)
    field(:date, Postiz.PrismaDateTime)
  end
end