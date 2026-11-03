defmodule Linkwarden.AppMigration do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Linkwarden.PrismaDateTime, autogenerate: {Linkwarden.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          status: atom(),
          finished_at: DateTime.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "AppMigration" do
    field(:name, :string)
    field(:status, Ecto.Enum, values: [:APPLIED, :PENDING, :FAILED])
    field(:finished_at, Linkwarden.PrismaDateTime, source: :finishedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end