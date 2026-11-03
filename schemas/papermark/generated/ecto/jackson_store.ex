defmodule Papermark.JacksonStore do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          key: String.t(),
          value: String.t(),
          iv: String.t() | nil,
          tag: String.t() | nil,
          namespace: String.t() | nil,
          modified_at: DateTime.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "jackson_store" do
    field(:key, :string, primary_key: true)
    field(:value, :string)
    field(:iv, :string)
    field(:tag, :string)
    field(:namespace, :string)
    field(:modified_at, Papermark.PrismaDateTime, source: :modifiedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end