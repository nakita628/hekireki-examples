defmodule OpenPanel.UserTotp do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          secret: String.t(),
          recovery_codes: [String.t()],
          enabled_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: OpenPanel.User.t() | nil
        }

  schema "user_totp" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:secret, :string)
    field(:recovery_codes, {:array, :string}, source: :recoveryCodes)
    field(:enabled_at, OpenPanel.PrismaDateTime, source: :enabledAt)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, OpenPanel.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end