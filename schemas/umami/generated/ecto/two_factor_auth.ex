defmodule Umami.TwoFactorAuth do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          secret: String.t(),
          is_enabled: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Umami.User.t() | nil
        }

  schema "two_factor_auth" do
    field(:id, :string, primary_key: true)
    field(:secret, :string)
    field(:is_enabled, :boolean, default: false)
    belongs_to(:user, Umami.User, foreign_key: :user_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end