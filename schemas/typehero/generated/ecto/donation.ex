defmodule TypeHero.Donation do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: TypeHero.PrismaDateTime, autogenerate: {TypeHero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          amount: integer(),
          stripe_checkout_session_id: String.t(),
          inserted_at: DateTime.t() | nil,
          user: TypeHero.User.t() | nil
        }

  schema "Donation" do
    field(:amount, :integer)
    field(:stripe_checkout_session_id, :string, source: :stripeCheckoutSessionId)
    field(:user_id, :binary_id, source: :userId)
    belongs_to(:user, TypeHero.User, foreign_key: :user_id, define_field: false, type: :binary_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end