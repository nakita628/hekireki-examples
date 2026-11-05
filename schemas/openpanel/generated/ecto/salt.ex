defmodule OpenPanel.Salt do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          salt: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "salts" do
    field(:salt, :string, primary_key: true)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end