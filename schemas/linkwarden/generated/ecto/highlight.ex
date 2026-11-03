defmodule Linkwarden.Highlight do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Linkwarden.PrismaDateTime, autogenerate: {Linkwarden.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          color: String.t(),
          comment: String.t() | nil,
          start_offset: integer(),
          end_offset: integer(),
          text: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          link: Linkwarden.Link.t() | nil,
          user: Linkwarden.User.t() | nil
        }

  schema "Highlight" do
    field(:color, :string)
    field(:comment, :string)
    field(:start_offset, :integer, source: :startOffset)
    field(:end_offset, :integer, source: :endOffset)
    field(:text, :string)
    field(:link_id, :id, source: :linkId)
    field(:user_id, :id, source: :userId)
    belongs_to(:link, Linkwarden.Link, foreign_key: :link_id, define_field: false)
    belongs_to(:user, Linkwarden.User, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end