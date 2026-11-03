defmodule Trigger.MfaBackupCode do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          code: String.t(),
          used_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Trigger.User.t() | nil
        }

  schema "MfaBackupCode" do
    field(:id, :string, primary_key: true)
    field(:code, :string)
    field(:used_at, Trigger.PrismaDateTime, source: :usedAt)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Trigger.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end