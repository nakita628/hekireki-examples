defmodule Umami.Share do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          entity_id: String.t(),
          name: String.t(),
          share_type: integer(),
          slug: String.t(),
          parameters: map(),
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil
        }

  schema "share" do
    field(:id, :string, primary_key: true, source: :share_id)
    field(:entity_id, :string)
    field(:name, :string)
    field(:share_type, :integer)
    field(:slug, :string)
    field(:parameters, :map)
    timestamps(inserted_at_source: :created_at)
  end
end