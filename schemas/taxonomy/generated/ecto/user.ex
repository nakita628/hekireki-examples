defmodule Taxonomy.User do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Taxonomy.PrismaDateTime, autogenerate: {Taxonomy.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          email: String.t() | nil,
          email_verified: DateTime.t() | nil,
          image: String.t() | nil,
          updated_at: DateTime.t(),
          stripe_customer_id: String.t() | nil,
          stripe_subscription_id: String.t() | nil,
          stripe_price_id: String.t() | nil,
          stripe_current_period_end: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          accounts: [Taxonomy.Account.t()],
          sessions: [Taxonomy.Session.t()],
          post: [Taxonomy.Post.t()]
        }

  schema "users" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:email, :string)
    field(:email_verified, Taxonomy.PrismaDateTime, source: :emailVerified)
    field(:image, :string)
    field(:updated_at, Taxonomy.PrismaDateTime, autogenerate: true)
    field(:stripe_customer_id, :string)
    field(:stripe_subscription_id, :string)
    field(:stripe_price_id, :string)
    field(:stripe_current_period_end, Taxonomy.PrismaDateTime)
    has_many(:accounts, Taxonomy.Account, foreign_key: :user_id, references: :id)
    has_many(:sessions, Taxonomy.Session, foreign_key: :user_id, references: :id)
    has_many(:post, Taxonomy.Post, foreign_key: :author_id, references: :id)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end