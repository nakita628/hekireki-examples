defmodule Umami.User do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          username: String.t(),
          password: String.t(),
          role: String.t(),
          logo_url: String.t() | nil,
          display_name: String.t() | nil,
          two_factor_required: boolean(),
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          two_factor_auth: Umami.TwoFactorAuth.t() | nil,
          two_factor_rate_limit: Umami.TwoFactorRateLimit.t() | nil,
          websites: [Umami.Website.t()],
          created_by: [Umami.Website.t()],
          links: [Umami.Link.t()],
          pixels: [Umami.Pixel.t()],
          teams: [Umami.TeamUser.t()],
          reports: [Umami.Report.t()],
          boards: [Umami.Board.t()],
          two_factor_backup_codes: [Umami.TwoFactorBackupCode.t()],
          two_factor_otp_useds: [Umami.TwoFactorOtpUsed.t()],
          api_keys: [Umami.ApiKey.t()]
        }

  schema "user" do
    field(:id, :string, primary_key: true, source: :user_id)
    field(:username, :string)
    field(:password, :string)
    field(:role, :string)
    field(:logo_url, :string)
    field(:display_name, :string)
    field(:two_factor_required, :boolean, default: false)
    field(:deleted_at, Umami.PrismaDateTime)
    has_one(:two_factor_auth, Umami.TwoFactorAuth, foreign_key: :user_id, references: :id)
    has_one(:two_factor_rate_limit, Umami.TwoFactorRateLimit, foreign_key: :user_id, references: :id)
    has_many(:websites, Umami.Website, foreign_key: :user_id, references: :id)
    has_many(:created_by, Umami.Website, foreign_key: :created_by, references: :id)
    has_many(:links, Umami.Link, foreign_key: :user_id, references: :id)
    has_many(:pixels, Umami.Pixel, foreign_key: :user_id, references: :id)
    has_many(:teams, Umami.TeamUser, foreign_key: :user_id, references: :id)
    has_many(:reports, Umami.Report, foreign_key: :user_id, references: :id)
    has_many(:boards, Umami.Board, foreign_key: :user_id, references: :id)
    has_many(:two_factor_backup_codes, Umami.TwoFactorBackupCode, foreign_key: :user_id, references: :id)
    has_many(:two_factor_otp_useds, Umami.TwoFactorOtpUsed, foreign_key: :user_id, references: :id)
    has_many(:api_keys, Umami.ApiKey, foreign_key: :user_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end