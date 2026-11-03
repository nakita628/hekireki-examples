defmodule Rallly.Session do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          expires_at: DateTime.t(),
          token: String.t(),
          ip_address: String.t() | nil,
          user_agent: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Rallly.User.t() | nil,
          impersonated_by_user: Rallly.User.t() | nil
        }

  schema "sessions" do
    field(:id, :string, primary_key: true)
    field(:expires_at, Rallly.PrismaDateTime)
    field(:token, :string)
    field(:ip_address, :string)
    field(:user_agent, :string)
    belongs_to(:user, Rallly.User, foreign_key: :user_id, type: :string)
    belongs_to(:impersonated_by_user, Rallly.User, foreign_key: :impersonated_by, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end