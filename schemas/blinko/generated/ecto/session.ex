defmodule Blinko.Session do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          sid: String.t(),
          data: String.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "session" do
    field(:id, :string, primary_key: true)
    field(:sid, :string)
    field(:data, :string)
    field(:expires_at, Blinko.PrismaDateTime, source: :expiresAt)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end