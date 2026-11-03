defmodule Postiz.Mentions do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          name: String.t(),
          username: String.t(),
          platform: String.t(),
          image: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "Mentions" do
    field(:name, :string, primary_key: true)
    field(:username, :string, primary_key: true)
    field(:platform, :string, primary_key: true)
    field(:image, :string, primary_key: true)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end