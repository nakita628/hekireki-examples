defmodule Jitsu.RateLimitCounter do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          bucket_key: String.t(),
          window_start: DateTime.t(),
          count: integer(),
          expires_at: DateTime.t()
        }

  schema "RateLimitCounter" do
    field(:bucket_key, :string, primary_key: true, source: :bucketKey)
    field(:window_start, Jitsu.PrismaDateTime, primary_key: true, source: :windowStart)
    field(:count, :integer, default: 0)
    field(:expires_at, Jitsu.PrismaDateTime, source: :expiresAt)
  end
end