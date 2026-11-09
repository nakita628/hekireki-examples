defmodule Jitsu.UserPassword do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          hash: String.t(),
          change_at_next_login: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Jitsu.UserProfile.t() | nil
        }

  schema "UserPassword" do
    field(:id, :string, primary_key: true)
    field(:hash, :string)
    field(:change_at_next_login, :boolean, default: false, source: :changeAtNextLogin)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Jitsu.UserProfile, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end