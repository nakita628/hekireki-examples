defmodule Taxonomy.Post do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Taxonomy.PrismaDateTime, autogenerate: {Taxonomy.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t(),
          content: map() | nil,
          published: boolean(),
          updated_at: DateTime.t(),
          inserted_at: DateTime.t(),
          author: Taxonomy.User.t() | nil
        }

  schema "posts" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:content, :map)
    field(:published, :boolean, default: false)
    field(:updated_at, Taxonomy.PrismaDateTime, autogenerate: true)
    field(:author_id, :string, source: :authorId)
    belongs_to(:author, Taxonomy.User, foreign_key: :author_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end