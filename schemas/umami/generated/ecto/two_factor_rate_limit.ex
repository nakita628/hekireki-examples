defmodule Umami.TwoFactorRateLimit do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          attempts: integer(),
          locked_until: DateTime.t() | nil,
          updated_at: DateTime.t(),
          user: Umami.User.t() | nil
        }

  schema "two_factor_rate_limit" do
    field(:id, :string, primary_key: true)
    field(:attempts, :integer, default: 0)
    field(:locked_until, Umami.PrismaDateTime)
    belongs_to(:user, Umami.User, foreign_key: :user_id, type: :string)
    timestamps(inserted_at: false)
  end
end