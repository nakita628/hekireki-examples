defmodule Hoppscotch.Shortcode do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Hoppscotch.PrismaDateTime, autogenerate: {Hoppscotch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          request: map(),
          embed_properties: map() | nil,
          created_on: DateTime.t(),
          updated_at: DateTime.t(),
          user: Hoppscotch.User.t() | nil
        }

  schema "Shortcode" do
    field(:id, :string, primary_key: true)
    field(:request, :map)
    field(:embed_properties, :map, source: :embedProperties)
    field(:created_on, Hoppscotch.PrismaDateTime, autogenerate: true, source: :createdOn)
    field(:creator_uid, :string, source: :creatorUid)
    belongs_to(:user, Hoppscotch.User, foreign_key: :creator_uid, define_field: false, type: :string, references: :uid)
    timestamps(inserted_at: false, updated_at_source: :updatedOn)
  end
end