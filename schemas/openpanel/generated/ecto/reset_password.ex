defmodule OpenPanel.ResetPassword do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          account: OpenPanel.Account.t() | nil
        }

  schema "reset_password" do
    field(:id, :string, primary_key: true)
    field(:expires_at, OpenPanel.PrismaDateTime, source: :expiresAt)
    field(:account_id, :string, source: :accountId)
    belongs_to(:account, OpenPanel.Account, foreign_key: :account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end