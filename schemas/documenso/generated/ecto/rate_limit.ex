defmodule Documenso.RateLimit do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          key: String.t(),
          action: String.t(),
          bucket: DateTime.t(),
          count: integer(),
          inserted_at: DateTime.t()
        }

  schema "RateLimit" do
    field(:key, :string, primary_key: true)
    field(:action, :string, primary_key: true)
    field(:bucket, Documenso.PrismaDateTime, primary_key: true)
    field(:count, :integer, default: 1)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end