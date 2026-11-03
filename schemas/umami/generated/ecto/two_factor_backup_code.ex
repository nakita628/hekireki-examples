defmodule Umami.TwoFactorBackupCode do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          code_hash: String.t(),
          used: boolean(),
          inserted_at: DateTime.t(),
          user: Umami.User.t() | nil
        }

  schema "two_factor_backup_code" do
    field(:id, :string, primary_key: true)
    field(:code_hash, :string)
    field(:used, :boolean, default: false)
    belongs_to(:user, Umami.User, foreign_key: :user_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end