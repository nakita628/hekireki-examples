defmodule Ghostfolio.User do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Ghostfolio.PrismaDateTime, autogenerate: {Ghostfolio.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          access_token: String.t() | nil,
          auth_challenge: String.t() | nil,
          provider: atom(),
          role: atom(),
          third_party_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          analytics: Ghostfolio.Analytics.t() | nil,
          settings: Ghostfolio.Settings.t() | nil,
          accesses_get: [Ghostfolio.Access.t()],
          accesses_give: [Ghostfolio.Access.t()],
          accounts: [Ghostfolio.Account.t()],
          activities: [Ghostfolio.Order.t()],
          api_keys: [Ghostfolio.ApiKey.t()],
          auth_devices: [Ghostfolio.AuthDevice.t()],
          subscriptions: [Ghostfolio.Subscription.t()],
          tags: [Ghostfolio.Tag.t()],
          symbol_profile: [Ghostfolio.SymbolProfile.t()],
          watchlist: [Ghostfolio.SymbolProfile.t()]
        }

  schema "User" do
    field(:access_token, :string, source: :accessToken)
    field(:auth_challenge, :string, source: :authChallenge)
    field(:provider, Ecto.Enum, values: [:ANONYMOUS, :GOOGLE, :INTERNET_IDENTITY, :OIDC], default: :ANONYMOUS)
    field(:role, Ecto.Enum, values: [:ADMIN, :DEMO, :INACTIVE, :USER], default: :USER)
    field(:third_party_id, :string, source: :thirdPartyId)
    has_one(:analytics, Ghostfolio.Analytics, foreign_key: :user_id)
    has_one(:settings, Ghostfolio.Settings, foreign_key: :user_id)
    has_many(:accesses_get, Ghostfolio.Access, foreign_key: :grantee_user_id)
    has_many(:accesses_give, Ghostfolio.Access, foreign_key: :user_id)
    has_many(:accounts, Ghostfolio.Account, foreign_key: :user_id)
    has_many(:activities, Ghostfolio.Order, foreign_key: :user_id)
    has_many(:api_keys, Ghostfolio.ApiKey, foreign_key: :user_id)
    has_many(:auth_devices, Ghostfolio.AuthDevice, foreign_key: :user_id)
    has_many(:subscriptions, Ghostfolio.Subscription, foreign_key: :user_id)
    has_many(:tags, Ghostfolio.Tag, foreign_key: :user_id)
    has_many(:symbol_profile, Ghostfolio.SymbolProfile, foreign_key: :user_id)
    many_to_many(:watchlist, Ghostfolio.SymbolProfile, join_through: "_UserWatchlist", join_keys: [B: :id, A: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end